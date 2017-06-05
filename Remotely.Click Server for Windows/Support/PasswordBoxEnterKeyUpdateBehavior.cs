using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Remotely.Click_Server_for_Windows.Support
{
    class PasswordBoxEnterKeyUpdateBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            if (this.AssociatedObject != null)
            {
                base.OnAttached();
                this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            }
        }

        protected override void OnDetaching()
        {
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
                base.OnDetaching();
            }
        }

        private void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                if (e.Key == Key.Return)
                {
                    if (e.Key == Key.Enter)
                    {
                        passwordBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    }
                }
            }
        }
    }
}
