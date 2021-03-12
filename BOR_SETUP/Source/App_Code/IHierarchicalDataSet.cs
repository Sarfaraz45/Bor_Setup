using System;
using System.Data;
using System.Web.UI;
using System.Collections;

namespace TreeViewBindingTest
{
    
    public class HierarchicalDataSet : IHierarchicalDataSource
    {
        DataSet dataSet;
        string idColumnName;
        string parentIdColumnName;

        
        public HierarchicalDataSet(DataSet dataSet, string idColumnName, string parentIdColumnName)
        {
            this.dataSet = dataSet;
            this.idColumnName = idColumnName;
            this.parentIdColumnName = parentIdColumnName;
        }

        public event EventHandler DataSourceChanged; // never used here

        public HierarchicalDataSourceView GetHierarchicalView(string viewPath)
        {
            return new DataSourceView(this, viewPath);
        }

        #region supporting methods
        DataRowView GetParentRow(DataRowView row)
        {
            //if (HasChildren(row))
            //{
                dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", idColumnName, row[parentIdColumnName].ToString());
                DataRowView parentRow = dataSet.Tables[0].DefaultView[0];
                dataSet.Tables[0].DefaultView.RowFilter = "";
                return parentRow;
            //}
            //else
            //{
 
            //}
        }

        string GetChildrenViewPath(string viewPath, DataRowView row)
        {
            return viewPath + "\\" + row[idColumnName].ToString();
        }

        bool HasChildren(DataRowView row)
        {
            //string aa = String.Format("{0} = {1}", parentIdColumnName, row[idColumnName]);
            dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", parentIdColumnName, row[idColumnName]);
            bool hasChildren = dataSet.Tables[0].DefaultView.Count > 0;
            dataSet.Tables[0].DefaultView.RowFilter = ""; 
            return hasChildren;
        }

        string GetParentViewPath(string viewPath)
        {
            return viewPath.Substring(0, viewPath.LastIndexOf("\\"));
        }
        #endregion

        #region private classes that implement further interfaces
        class DataSourceView : HierarchicalDataSourceView
        {
            HierarchicalDataSet hDataSet;
            string viewPath;

            public DataSourceView(HierarchicalDataSet hDataSet, string viewPath)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
            }

            public override IHierarchicalEnumerable Select()
            {
                return new HierarchicalEnumerable(hDataSet, viewPath);
            }
        }

        class HierarchicalEnumerable : IHierarchicalEnumerable
        {
            HierarchicalDataSet hDataSet;
            string viewPath;

            public HierarchicalEnumerable(HierarchicalDataSet hDataSet, string viewPath)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
            }

            public IHierarchyData GetHierarchyData(object enumeratedItem)
            {
                DataRowView row = (DataRowView)enumeratedItem;
                return new HierarchyData(hDataSet, viewPath, row);
            }

            public IEnumerator GetEnumerator()
            {
                if (viewPath == "")
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} is null", hDataSet.parentIdColumnName);
                else
                {
                    string lastID = viewPath.Substring(viewPath.LastIndexOf("\\") + 1);
                    hDataSet.dataSet.Tables[0].DefaultView.RowFilter = String.Format("{0} = {1}", hDataSet.parentIdColumnName, lastID);
                }

                IEnumerator i = hDataSet.dataSet.Tables[0].DefaultView.GetEnumerator();
                hDataSet.dataSet.Tables[0].DefaultView.RowFilter = "";
                return i;
            }
        }

        class HierarchyData : IHierarchyData
        {
            HierarchicalDataSet hDataSet;
            DataRowView row;
            string viewPath;

            public HierarchyData(HierarchicalDataSet hDataSet, string viewPath, DataRowView row)
            {
                this.hDataSet = hDataSet;
                this.viewPath = viewPath;
                this.row = row;
            }

            public IHierarchicalEnumerable GetChildren()
            {
                return new HierarchicalEnumerable(hDataSet, hDataSet.GetChildrenViewPath(viewPath, row));
            }

            public IHierarchyData GetParent()
            {
                return new HierarchyData(hDataSet, hDataSet.GetParentViewPath(viewPath), hDataSet.GetParentRow(row));
            }

            public bool HasChildren
            {
                get
                {
                    
                    //DataRowView rw = row;// new DataRowView();// row;
                    //string aa = "aaaaa" + row.Row.ItemArray[2];
                    //rw.Row.ItemArray[0] = row.Row.ItemArray[0];
                    //rw.Row.ItemArray[1] = row.Row.ItemArray[1];
                    //rw.Row.ItemArray[2] = "aaaaa"+row.Row.ItemArray[2];
                    //row = rw;
                    //row.Row[2].ToString() = "Abc1".ToString();
                    //row.Row.ItemArray[2] = row.Row.ItemArray[2] + "aaaaa";
                    return hDataSet.HasChildren(row);
                }
            }

            public object Item
            {
                get
                {
                    
                    return row;
                }
            }

            public string Path
            {
                get
                {
                    return viewPath;
                }
            }

            public string Type
            {
                get
                {
                    return typeof(DataRowView).ToString();
                }
            }
        }
        #endregion
    }
}
