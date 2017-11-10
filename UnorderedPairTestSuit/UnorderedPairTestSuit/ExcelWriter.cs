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

        const int startX = 3;
        const int startY = 6;

        const int endX = startX + 10;

        const int nextTableY = 9;

        int currentX = startX;
        int currentY = startY;

        public ExcelWriter(string filename)
        {
            m_workbook = new XLWorkbook(filename);
            m_sheet = m_workbook.Worksheets.Worksheet("sheet1");
        }

        public void WriteTestData(TestData data)
        {
            m_sheet.Cell(currentY, currentX).Value = data.m_minimum.TotalMilliseconds.ToString();
            m_sheet.Cell(currentY + 1, currentX).Value = data.m_maximum.TotalMilliseconds.ToString();
            m_sheet.Cell(currentY + 2, currentX).Value = data.m_average.TotalMilliseconds.ToString();
            m_workbook.Save();

            currentX++;
            if (currentX == endX)
            {
                currentX = startX;
                currentY += nextTableY;
            }
        }

        public void SetWritePosition(int offset = 0)
        {
            currentX = startX;
            currentY = startY + (nextTableY * offset);
        }
    }
}
