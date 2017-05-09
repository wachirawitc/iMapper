using System;
using System.Threading;
using System.Windows.Forms;

namespace iMapper.Forms
{
    public partial class LoadForm : Form
    {
        private readonly MethodInvoker method;

        public LoadForm(MethodInvoker action)
        {
            InitializeComponent();
            method = action;
        }

        private void OnLoadForm(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                method.Invoke();
                InvokeAction(this, Dispose);
            }).Start();
        }

        public static void InvokeAction(Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
    }
}