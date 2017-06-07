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

            /*comboBox1.Items.AddRange(new string[] {
                ReplaceWhere.Before.ToString(),
                ReplaceWhere.Replace.ToString(),
                ReplaceWhere.After.ToString()
            });*/
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                ReadAndReplace(
                    textBox1.Text,
                    openFileDialog1.FileName.Substring(0, openFileDialog1.FileName.LastIndexOf('\\') + 1),
                    openFileDialog1.SafeFileName,
                    //comboBox1.SelectedIndex == -1 ? (ReplaceWhere)2 : (ReplaceWhere)comboBox1.SelectedIndex,
                    checkBox1.Checked
                );
        }

        public void ReadAndReplace(string word, string filePath, string fileName, /*ReplaceWhere where,*/ bool overrideFile)
        {
            try
            {
                var fileText = new string[0];

                /*using (var reader = new StreamReader(filePath + fileName))
                    fileText = reader.ReadToEnd().Replace(word,
                        (where == ReplaceWhere.After ? word : "") +
                        ";" +
                        (where == ReplaceWhere.Before ? word : ""));*/

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

    /*public enum ReplaceWhere
    {
        Before,
        Replace,
        After
    }*/
}
