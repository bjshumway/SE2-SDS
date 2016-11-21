using System;
using System.Windows.Forms;

namespace TranslationTest {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            // MessageBox just to show what status code we got
            MessageBox.Show(
                "ioStatusCode: " + 
                Enum.GetName(typeof(MLH.ioStatusCode),
                (int)MLH.populateDict("french")) // initialize the dict, filename -> 'french'
            );
        }

        private void chkEnglish_CheckedChanged(object sender, EventArgs e) {

            // looping through all Controls
            foreach (Control c in this.Controls) {

                // checking for Labels
                if (c.GetType() == typeof(Label)) {
                    var label = c as Label;

                    // translating label's text based on whether chkEnglish is checked
                    label.Text = MLH.tr(label.Text, !chkEnglish.Checked);
                }
            }
        }
    }
}
