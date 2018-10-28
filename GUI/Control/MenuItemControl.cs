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
using BLL;

namespace GUI.Control
{
    public partial class MenuItemControl : UserControl
    {
        public delegate void OnDeleteHandler(DAL.MenuItem menuItem);
        public event OnDeleteHandler OnDelete;

        public delegate void OnEditHandler(MenuItemControl menuItemControl);
        public event OnEditHandler OnEdit;

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

        bool readOnly = true;
        public bool ReadOnly {
            get { return readOnly;  }
            set
            {
                readOnly = value;
                this.ContextMenuStrip = readOnly ? null : this.contextMenuStrip;
            }
        }

        public MenuItemControl()
        {
            InitializeComponent();
            AddEvent();
        }

        public MenuItemControl(DAL.MenuItem menuItem, bool readOnly = true)
        {
            InitializeComponent();
            AddEvent();

            this.MenuItem = menuItem;
            this.ReadOnly = readOnly;
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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete \"" + this.MenuItem.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                MenuItemBLL menuItemBLL = new MenuItemBLL();
                menuItemBLL.Delete(this.MenuItem);

                // fire event
                if (this.OnDelete != null)
                    this.OnDelete(this.MenuItem);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.OnEdit != null)
                this.OnEdit(this);
        }
    }
}
