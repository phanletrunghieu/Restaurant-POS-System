using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.Utilities;

namespace GUI.Control
{
    public partial class MenuItemControl : UserControl
    {
        int hover;

        private DAL.MenuItem menuItem;
        public DAL.MenuItem MenuItem
        {
            get { return menuItem; }
            set
            {
                menuItem = value;
                this.lbName.Text = menuItem.Name;
                this.lbPrice.Text = menuItem.Price.ToString();
                this.pictureBox.Image = UtilsImage.ByteArrayToImage(menuItem.Image);
            }
        }

        public MenuItemControl()
        {
            InitializeComponent();
            AddEvent();
        }

        public MenuItemControl(DAL.MenuItem menuItem)
        {
            InitializeComponent();
            AddEvent();

            this.MenuItem = menuItem;
        }

        private void AddEvent()
        {
            for(int i = 0; i<this.Controls.Count; i++)
            {
                this.Controls[i].MouseEnter += new EventHandler((object sender, EventArgs e) => this.OnMouseEnter(e));
                this.Controls[i].MouseLeave += new EventHandler((object sender, EventArgs e) => this.OnMouseLeave(e));
                this.Controls[i].Click += new EventHandler((object sender, EventArgs e) => this.OnClick(e));
            }
        }

        private void MenuItemControl_MouseEnter(object sender, EventArgs e)
        {
            this.hover++;
            if (this.hover > 0)
            {
                this.BackColor = Color.WhiteSmoke;
            }
        }

        private void MenuItemControl_MouseLeave(object sender, EventArgs e)
        {
            this.hover--;
            if (this.hover <= 0)
            {
                this.BackColor = Color.Transparent;
            }
        }
    }
}
