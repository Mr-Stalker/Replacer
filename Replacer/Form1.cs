using System;
using System.IO;
using System.Windows.Forms;

namespace Replacer
{
    public partial class Form1 : Form
    {
        int[] positions = new int[]{
            245, 242, 230, 217, 210, 202, 201, 188, 170, 152, 147, 134, 121, 108, 95, 82, 69, 56, 52, 49, 39, 27, 24, 12, 10, 02
        };

        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] {
                "Before",
                "Replace",
                "After"
            });
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                ReadAndReplace(
                    textBox1.Text,
                    openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\') + 1),
                    openFileDialog1.SafeFileName,
                    comboBox1.SelectedIndex == -1 ? 2 : comboBox1.SelectedIndex,
                    checkBox1.Checked
                );
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                ReadAndReplacePattern(
                    textBox1.Text,
                    openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\') + 1),
                    openFileDialog1.SafeFileName,
                    checkBox1.Checked
                );
        }

        private void ReadAndReplace(string word, string filePath, string fileName, int where, bool overrideFile)
        {
            try
            {
                var fileText = "";

                using (var reader = new StreamReader(filePath + fileName))
                    fileText = reader.ReadToEnd().Replace(word, (where == 2 ? word : "") + ";" + (where == 0 ? word : ""));

                using (var writer = new StreamWriter(filePath + (overrideFile ? "" : "out") + fileName))
                    writer.Write(fileText);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("O arquivo não foi encontrado", "Erro");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro, tente novamente", "Erro");
                return;
            }

            MessageBox.Show("Processo completado", "Sucesso");
        }

        private void ReadAndReplacePattern(string word, string filePath, string fileName, bool overrideFile)
        {
            try
            {
                var fileText = new string[0];

                using (var reader = new StreamReader(filePath + fileName))
                {
                    fileText = reader.ReadToEnd().Split('\n');

                    for (int i = 0; i < fileText.Length; i++)
                        foreach (var item in positions)
                            fileText[i] = fileText[i].Insert(item, ";");
                }

                using (var writer = new StreamWriter(filePath + (overrideFile ? "" : "out") + fileName))
                    writer.Write(String.Join("\n", fileText));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("O arquivo não foi encontrado", "Erro");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Houve um erro, tente novamente", "Erro");
                return;
            }

            MessageBox.Show("Processo completado", "Sucesso");
        }
    }
}
