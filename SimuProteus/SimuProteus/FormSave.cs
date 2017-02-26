﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuProteus
{
    public partial class FormSave : Form
    {
        public Action<int, string> HandlerAfterSave
        {
            get;
            set;
        }

        /// <summary>
        /// 选中的项目
        /// </summary>
        public ProjectDetails Project
        {
            get;
            set;
        }
        /// <summary>
        /// 新建还是修改
        /// </summary>
        public bool NewFlag
        {
            get;
            set;
        }

        public FormSave()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.tbName.Text.Trim().Equals(string.Empty))
            {
                MessageBox.Show("请输入项目名称");
                return;
            }
            this.Project.Project.Name = this.tbName.Text.Trim();
            this.Project.Project.Description = this.tbDesc.Text.Trim();
            this.Project.Project.CreateTime = DateTime.Now;
            this.Project.Project.UpdateTime = DateTime.Now;

            DBUtility dbhandler = new DBUtility();
            if (dbhandler.CheckProjectNameExists(this.Project.Project.Name))
            {
                MessageBox.Show("名称已存在，请换个名称");
                return;
            }
            int result = dbhandler.InsertNewProject(this.Project);
            if (result <= 0)
            {
                MessageBox.Show("保存失败");
            }
            else
            {
                HandlerAfterSave(result, this.Project.Project.Name);
                MessageBox.Show("保存成功");
                this.Close();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}