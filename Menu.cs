using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public class Menu : Form
    {
        public Menu()
        {
            var buttonStart = new Button();
            buttonStart.Text = "Start";
            buttonStart.Location = new Point(1, 1);
            buttonStart.Click += (sender, args) =>
            {
                this.Hide();
                var w = new MyForm();
                w.Show();
            };
            this.Controls.Add(buttonStart);
        }
    }
}