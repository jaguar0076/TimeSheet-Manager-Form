using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace TimeSheet_Manager_Form
{
    class Excel_Utils
    {
        #region Getters/Setters

        private string FileName { get; set; }

        private string SheetName { get; set; }

        private List<string> LRange { get; set; }

        private Microsoft.Office.Interop.Excel.Application Xapp { get; set; }

        private Microsoft.Office.Interop.Excel.Workbook Wbook { get; set; }

        private Microsoft.Office.Interop.Excel.Worksheet WSheet { get; set; }

        #endregion

        #region Constructor

        protected Excel_Utils()
        {
            try
            {
                Xapp = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception)
            { }
        }

        public Excel_Utils(string FileName)
            : this()
        {
            this.FileName = FileName;

            Wbook = Xapp.Workbooks.Open(this.FileName, Type.Missing, Type.Missing, Type.Missing,
                                                                             Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                             Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                             Type.Missing, Type.Missing, Type.Missing);
        }

        public Excel_Utils(string FileName, string SheetName)
            : this(FileName)
        {
            this.SheetName = SheetName;

            WSheet = Wbook.Sheets[SheetName];
        }

        #endregion

        #region Functions

        public List<string> ReadRange(string MinRange, string MaxRange)
        {
            Range range = WSheet.get_Range(MinRange, MaxRange);

            List<object> excelRowValues = GetRowValues((Array)range.Cells.Value);

            //List<object> objects = new List<object>();
            LRange = (from o in excelRowValues
                      select o.ToString()).ToList();

            return LRange;
        }

        /*public List<string> ReadRange(int MinRange, int MaxRange)
        {
            return LRange;
        }*/

        // This method will convert the array of values from the row into a list of objects.
        private List<object> GetRowValues(Array excelRow)
        {
            // If the first cell does not have a value, return null
            if (excelRow.GetValue(1, 1) == null)
                return null;

            // Add each item in the row to the list
            List<object> values = new List<object>();

            for (int i = 1; i <= excelRow.Length; i++)
                values.Add(excelRow.GetValue(i, 1));

            return values;
        }

        #endregion
    }
}