//using System;
//using System.Collections.Generic;
//using System.Text;
//using FreeSql.Generator.Core.CodeFirst;

//namespace FreeSql.Generator.Core.Utilities
//{
//    public static class GloabalTableInfo
//    {
//        public static void Clear()
//        {
//            if (_allTableInfos != null)
//                _allTableInfos.Clear();
//        }
//        public static void Add(this TableInfo info)
//        {
//            if (_allTableInfos == null)
//            {
//                _allTableInfos = new List<TableInfo>();
//            }
//            _allTableInfos.Add(info);
//        }
//        private static List<TableInfo> _allTableInfos;
//        public static List<TableInfo> AllTableInfos => _allTableInfos;
//    }
//}
