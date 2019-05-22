using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;

using MappingTools.Generator;

namespace MappingTools
{
    public partial class FormMain : Form
    {
        private ORMapper mapper;

        private DbTableReader tableReader;
        private CSharpClassWriter classWriter;
        private CSharpClassLogicWriter classLogicWriter;
        private CSharpClassQueryParaWriter queryParaWriter;
        private XmlMapWriter mapWriter;

        public FormMain()
        {
            InitializeComponent();


            btnRefresTables_Click(null, null);
        }

        private void btnGenerating_Click(object sender, EventArgs e)
        {
            string nameSpace = txtNameSpace.Text;
            string assemblyName = txtAssemblyName.Text;
            string entityNameSpace, entityAssemblyName, queryParaNameSpace;
            string entityPath, queryParaPath, xmlPath;
            //ATA.Biz.Entity
            if (nameSpace.EndsWith(".entity", StringComparison.CurrentCultureIgnoreCase))
                nameSpace = nameSpace.Substring(0, nameSpace.Length - 7);
            if (assemblyName.EndsWith(".entity", StringComparison.CurrentCultureIgnoreCase))
                assemblyName = assemblyName.Substring(0, assemblyName.Length - 7);

            entityNameSpace = string.Concat(nameSpace, ".Entity");
            queryParaNameSpace = string.Concat(nameSpace, ".Interface");
            entityAssemblyName = string.Concat(assemblyName, ".Entity");

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(txtClassPath.Text);
            if (dir.Exists)
                dir.Delete(true);
            dir.Create();

            entityPath = dir.CreateSubdirectory("entity").FullName;
            xmlPath = dir.CreateSubdirectory("ormaping").FullName;
            queryParaPath = dir.CreateSubdirectory("querypara").FullName;

            foreach (string tableName in lstTables.CheckedItems)
            {
                mapper.NameSpace = entityNameSpace;
                mapper.AssemblyName = entityAssemblyName;
                mapper.LogicNameSpace = txtLogicNameSpace.Text;

                tableReader.TableName = tableName;// cmbTableName.Text;
                classWriter.BasePath = entityPath;
                classWriter.TableName = tableName;// cmbTableName.Text;
                classLogicWriter.BasePath = txtLogicPath.Text;

                queryParaWriter.BasePath = queryParaPath;
                queryParaWriter.TableName = tableName;
                queryParaWriter.NameSpace = queryParaNameSpace;

                mapWriter.BasePath = xmlPath;

                mapper.Generating(tableReader, classWriter, chkGenHelper.Checked ? classLogicWriter : null, mapWriter);
                mapper.Generating(tableReader, queryParaWriter);
            }
            MessageBox.Show("Map successfully generated!");
        }

        private void btnClassPath_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgFolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtClassPath.Text = dlgFolderBrowser.SelectedPath;
            }
        }

        private void btnMapPath_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgFolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtMapPath.Text = dlgFolderBrowser.SelectedPath;
            }
        }

        private void btnLogicPath_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgFolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                txtLogicPath.Text = dlgFolderBrowser.SelectedPath;
            }
        }

        private void chkGenHelper_CheckedChanged(object sender, EventArgs e)
        {
            txtLogicNameSpace.Enabled = chkGenHelper.Checked;
            txtLogicPath.Enabled = chkGenHelper.Checked;
        }

        private void btnRefreshData_Click(object sender, EventArgs e)
        {
            Misc.RefreshComment();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTables.Items.Count; i++)
                lstTables.SetItemChecked(i, true);
        }

        private void btnReverseSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTables.Items.Count; i++)
                lstTables.SetItemChecked(i, !lstTables.GetItemChecked(i));
        }

        private void btnRefresTables_Click(object sender, EventArgs e)
        {
            lstTables.Items.Clear();
            mapper = new ORMapper();
            Database database = DatabaseFactory.CreateDatabase();
            tableReader = new DbTableReader(database);
            classWriter = new CSharpClassWriter();
            classLogicWriter = new CSharpClassLogicWriter();
            mapWriter = new XmlMapWriter();
            queryParaWriter = new CSharpClassQueryParaWriter();
            lstTables.Items.AddRange(Misc.GetDbTables(database));
        }
    }
}