using System.Collections.Generic;
using HandyControl.Controls;
using HandyControl.Data;

namespace WpfFreeSqlBuilder.ViewModal
{
    public class MainViewModal
    {
        private SideMenu _sideMenus;

        public MainViewModal()
        {
            this.Sider = new SideMenu()
            {
                ExpandMode = ExpandMode.ShowOne,
                ItemsSource = new List<SideMenuItem>()
                    {
                        new SideMenuItem()
                        {
                            Header = "打开",
                            ItemsSource = new List<SideMenuItem>
                            {
                                new SideMenuItem()
                                {
                                    Name = "Project",
                                    Header = "项目",
                                },
                                new SideMenuItem()
                                {
                                    Name = "DataSource",
                                    Header = "数据源",
                                }, new SideMenuItem()
                                {
                                    Name = "Builder",
                                    Header = "构建器",
                                }, new SideMenuItem()
                                {
                                    Name="Template",
                                    Header = "模板",
                                },
                            }
                        }
                    }
                //DataContext = new List<SideMenuItem>
                //{
                //    new SideMenuItem()
                //    {
                //        Header = "项目",
                //    },
                //    new SideMenuItem()
                //    {
                //        Header = "数据源",
                //    }, new SideMenuItem()
                //    {
                //        Header = "构建器",
                //    }, new SideMenuItem()
                //    {
                //        Header = "模板",
                //    },
                //}

            };
        }

        public int SelectedIndex { get; set; }

        public SideMenu Sider
        {
            get => _sideMenus;
            set
            {
                _sideMenus = value;
            }
        }
    }


}