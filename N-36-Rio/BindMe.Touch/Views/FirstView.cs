using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace BindMe.Touch.Views
{
    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
        public override void ViewDidLoad()
        {
            View = new UIView(){ BackgroundColor = UIColor.White};
            base.ViewDidLoad();

            var textFieldTitle = new UITextField(new RectangleF(10, 10, 300, 30));
            Add(textFieldTitle);
            var picker = new UIPickerView();
            var pickerViewModel = new MvxPickerViewModel(picker);
            picker.Model = pickerViewModel;
            picker.ShowSelectionIndicator = true; 
            textFieldTitle.InputView = picker;

            var textFieldFirstName = new UITextField(new RectangleF(10, 40, 300, 30));
            Add(textFieldFirstName);
            var textFieldLastName = new UITextField(new RectangleF(10, 70, 300, 30));
            Add(textFieldLastName);
            var acceptedLabel = new UILabel(new RectangleF(10, 100, 200, 30));
            acceptedLabel.Text = "Accepted?";
            Add(acceptedLabel);
            var accepted = new UISwitch(new RectangleF(210, 100, 100, 30));
            Add(accepted);
            var add = new UIButton(UIButtonType.RoundedRect);
            add.SetTitle("Add", UIControlState.Normal);
            add.TintColor = UIColor.Purple;
            add.Frame = new RectangleF(10,130,300,30);
            Add(add);

            var table = new UITableView(new RectangleF(10, 160, 300, 300));
            Add(table);
            var source = new MvxStandardTableViewSource(table, "TitleText FirstName");
            table.Source = source;

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(textFieldFirstName).To(vm => vm.FirstName);
            set.Bind(textFieldLastName).To(vm => vm.LastName);
            set.Bind(pickerViewModel).For(p => p.ItemsSource).To(vm => vm.Titles);
            set.Bind(pickerViewModel).For(p => p.SelectedItem).To(vm => vm.Title);
            set.Bind(textFieldTitle).To(vm => vm.Title);
            set.Bind(accepted).To(vm => vm.Accepted);
            set.Bind(add).To("Add");
            set.Bind(source).To(vm => vm.People);
            set.Apply();

            var tap = new UITapGestureRecognizer(() =>
                {
                    foreach (var view in View.Subviews)
                    {
                        var text = view as UITextField;
                        if (text != null)
                            text.ResignFirstResponder();
                    }
                });
            View.AddGestureRecognizer(tap);
        }
    }
}