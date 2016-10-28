using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StripNameSpace {
    public partial class Form1 : Form {

        const string OMIT = "[SNS]DO NOT WRITE THIS";
        string errors = "";
        string nsName = "";

        List<string> lst = new List<string>();

        public Form1() {
            InitializeComponent();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e) {
            foreach (string file in ofd.FileNames) {
                lstFiles.Items.Add(file);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            ofd.ShowDialog();
        }

        private void btnStrip_Click(object sender, EventArgs e) {
            errors = "";
            nsName = txtNS.Text;
            btnOpen.Enabled  = false;
            btnStrip.Enabled = false;

            foreach (string s in lstFiles.Items) {
                lst.Add(s);
            }

            bgw.RunWorkerAsync();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            foreach (string s in lst) {
                try {
                    string fileText = "";

                    using (StreamReader r = File.OpenText(s)) {
                        fileText = r.ReadToEnd(); // dump the file content into fileText
                    }

                    string[] lines = fileText.Replace("\r", "").Split('\n');

                    bool foundNS = false;
                    for (int x = 0; x < lines.Length; x++) {
                        if (lines[x].Trim().StartsWith("namespace " + nsName)) {
                            lines[x] = OMIT; // mark the NS line for omittance
                            if (lines[x + 1].Trim().StartsWith("{")) {
                                lines[x + 1] = lines[x + 1].Remove(lines[x + 1].IndexOf('{'), 1);
                            }

                            foundNS  = true;
                            break;
                        }
                    }

                    if (foundNS) { // we have found and omitted the NS
                        for (int x = 0; x < lines.Length; x++) {
                            if (lines[x].StartsWith("    ")) {
                                lines[x] = lines[x].Remove(0, 4); // remove 4 leading spaces
                            }
                        }

                        StringBuilder sb = new StringBuilder();

                        foreach (string line in lines) {
                            if (line != OMIT) {
                                sb.Append(line);
                                sb.Append("\r\n");
                            }
                        }

                        fileText = sb.ToString().TrimEnd('\r', '\n');

                        // remove last '}'
                        int lastBracketIndex = fileText.LastIndexOf('}');
                        fileText = fileText.Remove(lastBracketIndex, 1);

                        using (StreamWriter w = File.CreateText(s)) {
                            w.Write(fileText);
                        }
                    }

                } catch {
                    errors += s + "\r\n";
                }
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (errors != "") {
                MessageBox.Show("Couldn't strip the following file(s):\r\n" + errors);
            } else {
                MessageBox.Show("All stripped");
            }

            btnOpen.Enabled  = true;
            btnStrip.Enabled = true;
            lstFiles.Items.Clear();
            lst.Clear();
        }
    }
}
