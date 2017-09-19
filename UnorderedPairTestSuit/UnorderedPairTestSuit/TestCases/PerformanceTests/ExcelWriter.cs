using ClosedXML.Excel;
using System;

namespace UnorderedPairTestSuit
{
    class ExcelWriter
    {
        public struct TestData
        {
            public string m_testName;

            public TimeSpan m_average;
            public TimeSpan m_minimum;
            public TimeSpan m_maximum;
        }

        XLWorkbook m_workbook;
        IXLWorksheet m_sheet;

        public ExcelWriter(string filename)
        {
            m_workbook = new XLWorkbook(filename);
            m_sheet = m_workbook.Worksheets.Worksheet("sheet1");
        }

        public void WriteTestData(TestData data)
        {
            m_sheet.Cell(2, 5).Value = data.m_testName;
            m_workbook.Save();
        }
    }
}
