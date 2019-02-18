using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace FileHelper
{
    public static class ExcelHelper
    {
        #region 导出列表

        /// <summary>
        /// 导出excel
        /// 有性能影响
        /// XLSX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static Stream ExportExcel<T>(List<T> data, List<ExcelCell> columns)
        {
            //获得属性
            PropertyInfo[] property = typeof(T).GetProperties();//模型类字段
            //创建Excel文件对象
            XSSFWorkbook book = new XSSFWorkbook();
            //添加一个Sheet
            ISheet sheet = book.CreateSheet("Sheet1");
            //设置首行标题
            int columnIndex = 0, rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex++);
            foreach (var column in columns)
            {
                row.CreateCell(columnIndex++).SetCellValue(column.ShowName);
            }
            //设置样式
            ICellStyle style = book.CreateCellStyle();
            IDataFormat dataformat = book.CreateDataFormat();
            style.DataFormat = dataformat.GetFormat("yyyy/m/d h:mm");
            List<int> dateColumn = new List<int>();
            //设置数据
            foreach (var d in data)
            {
                row = sheet.CreateRow(rowIndex++);
                columnIndex = 0;
                foreach (var ep in columns)
                {
                    PropertyInfo temp = property.FirstOrDefault(p => p.Name == ep.PropertyName);
                    if (temp != null)
                    {
                        #region 赋值
                        switch (temp.PropertyType.Name)
                        {
                            case "Double":
                                row.CreateCell(columnIndex++).SetCellValue((double)temp.GetValue(d));
                                break;
                            case "Int32":
                                row.CreateCell(columnIndex++).SetCellValue((int)temp.GetValue(d));
                                break;
                            case "Int64":
                                row.CreateCell(columnIndex++).SetCellValue((int)temp.GetValue(d));
                                break;
                            case "Boolean":
                                row.CreateCell(columnIndex++).SetCellValue((bool)temp.GetValue(d));
                                break;
                            case "DateTime":
                                if (rowIndex == 2)
                                    dateColumn.Add(columnIndex);
                                row.CreateCell(columnIndex).SetCellValue((DateTime)temp.GetValue(d));
                                row.GetCell(columnIndex++).CellStyle = style;
                                break;
                            default:
                                row.CreateCell(columnIndex++).SetCellValue((string)temp.GetValue(d));
                                break;
                        }
                        #endregion
                    }
                }
            }
            foreach (var column in dateColumn)
            {
                sheet.SetColumnWidth(column, 15 * 256);
            }
            NpoiMemoryStream ms = new NpoiMemoryStream { AllowClose = false };
            book.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return ms;
        }

        /// <summary>
        /// 导出excel
        /// 有性能影响
        /// XLSX
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static XSSFWorkbook ExportExcelBook<T>(List<T> data, List<ExcelCell> columns)
        {
            //获得属性
            PropertyInfo[] property = typeof(T).GetProperties();//模型类字段
            //创建Excel文件对象
            XSSFWorkbook book = new XSSFWorkbook();
            //添加一个Sheet
            ISheet sheet = book.CreateSheet("Sheet1");
            //设置首行标题
            int columnIndex = 0, rowIndex = 0;
            IRow row = sheet.CreateRow(rowIndex++);
            foreach (var column in columns)
            {
                row.CreateCell(columnIndex++).SetCellValue(column.ShowName);
            }
            //设置样式
            ICellStyle style = book.CreateCellStyle();
            IDataFormat dataformat = book.CreateDataFormat();
            style.DataFormat = dataformat.GetFormat("yyyy/m/d h:mm");
            List<int> dateColumn = new List<int>();
            //设置数据
            foreach (var d in data)
            {
                row = sheet.CreateRow(rowIndex++);
                columnIndex = 0;
                foreach (var ep in columns)
                {
                    PropertyInfo temp = property.FirstOrDefault(p => p.Name == ep.PropertyName);
                    if (temp != null)
                    {
                        #region 赋值
                        switch (temp.PropertyType.Name)
                        {
                            case "Double":
                                row.CreateCell(columnIndex++).SetCellValue((double)temp.GetValue(d));
                                break;
                            case "Int32":
                                row.CreateCell(columnIndex++).SetCellValue((int)temp.GetValue(d));
                                break;
                            case "Int64":
                                row.CreateCell(columnIndex++).SetCellValue((int)temp.GetValue(d));
                                break;
                            case "Boolean":
                                row.CreateCell(columnIndex++).SetCellValue((bool)temp.GetValue(d));
                                break;
                            case "DateTime":
                                if (rowIndex == 2)
                                    dateColumn.Add(columnIndex);
                                row.CreateCell(columnIndex).SetCellValue((DateTime)temp.GetValue(d));
                                row.GetCell(columnIndex++).CellStyle = style;
                                break;
                            default:
                                row.CreateCell(columnIndex++).SetCellValue((string)temp.GetValue(d));
                                break;
                        }
                        #endregion
                    }
                }
            }
            foreach (var column in dateColumn)
            {
                sheet.SetColumnWidth(column, 15 * 256);
            }
            return book;
        }

        #endregion

        #region 模板导出

        /// <summary>
        /// 导出一条数据
        /// {属性名}对应填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="excel">文件流</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Stream ExportTemplate<T>(T model, Stream excel, ExcelType type)
        {
            //获得属性
            PropertyInfo[] property = typeof(T).GetProperties();//模型类字段
            //读取excel
            IWorkbook workbook = GetWorkBook(excel, type);
            //读取sheet1
            ISheet sheet = workbook.GetSheetAt(0);
            //读取数据
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                var dataRow = sheet.GetRow(i);
                if (dataRow != null)
                {
                    for (int j = 0; j < dataRow.LastCellNum; j++)
                    {
                        ICell cell = sheet.GetRow(i).GetCell(j);
                        //找到待替换数据格
                        if (cell != null && cell.StringCellValue.IndexOf("{", StringComparison.Ordinal) >= 0 && cell.StringCellValue.IndexOf("}", StringComparison.Ordinal) >= 0)
                        {
                            PropertyInfo temp = property.FirstOrDefault(p => ("{" + p.Name + "}") == cell.StringCellValue);
                            if (temp != null)
                            {
                                cell.SetCellValue(temp.GetValue(model).ToString());
                            }
                        }
                    }
                }
            }
            NpoiMemoryStream ms = new NpoiMemoryStream { AllowClose = false };
            workbook.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            return ms;
        }

        /// <summary>
        /// 导出一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="excel">文件路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Stream ExportTemplate<T>(T model, string excel, ExcelType type)
        {
            using (Stream stream = File.OpenRead(excel))
            {
                return ExportTemplate(model, stream, type);
            }
        }

        /// <summary>
        /// 导出一组数据
        /// {属性名}对应填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="excel">文件流</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Stream> ExportTemplateAny<T>(List<T> model, Stream excel, ExcelType type)
        {
            return model.Select(item => ExportTemplate(item, excel, type)).ToList();
        }

        /// <summary>
        /// 导出一组数据
        /// {属性名}对应填充
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="excel">文件路径</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<Stream> ExportTemplateAny<T>(List<T> model, string excel, ExcelType type)
        {
            using (Stream stream = File.OpenRead(excel))
            {
                return model.Select(item => ExportTemplate(item, stream, type)).ToList();
            }
        }

        #endregion

        #region 列表读取

        /// <summary>
        /// 从excel上读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excel">excel模板数据</param>
        /// <param name="type">excel类型</param>
        /// <param name="columns">key：显示名，value：属性名</param>
        /// <returns></returns>
        public static List<T> ImportData<T>(Stream excel, ExcelType type, Dictionary<string, string> columns) where T : new()
        {
            //读取excel
            IWorkbook workbook = GetWorkBook(excel, type);
            //读取sheet1
            ISheet sheet = workbook.GetSheetAt(0);
            //读取表头
            IRow top = sheet.GetRow(0);
            //创建json结构
            List<string> dataList = new List<string>();
            //读取数据
            for (int i = 1; i <= sheet.LastRowNum; i++)
            {
                List<string> data = new List<string>();
                var datarow = sheet.GetRow(i);
                for (int j = 0; j < top.LastCellNum; j++)
                {
                    var name = columns[GetValue(top.GetCell(j)).ToString()];
                    var value = GetValue(datarow.GetCell(j));
                    var item = "\"" + name + "\"" + ":" + "\"" + value + "\"";
                    data.Add(item);
                }
                var dataStr = "{" + string.Join(",", data) + "}";
                dataList.Add(dataStr);
            }
            var dataListStr = "[" + string.Join(",", dataList) + "]";
            //json反序列化导出
            List<T> result = JsonConvert.DeserializeObject<List<T>>(dataListStr);
            return result;
        }

        /// <summary>
        /// 从excel上读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="excel">excel模板数据</param>
        /// <param name="type">excel类型</param>
        /// <param name="columns">key：显示名，value：属性名</param>
        /// <returns></returns>
        public static List<T> ImportData<T>(string excel, ExcelType type, Dictionary<string, string> columns) where T : new()
        {
            using (Stream stream = File.OpenRead(excel))
            {
                return ImportData<T>(stream, type, columns);
            }
        }

        #endregion

        #region excel辅助方法

        /// <summary>
        /// 获得工作表
        /// </summary>
        /// <param name="excel"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IWorkbook GetWorkBook(Stream excel, ExcelType type)
        {
            IWorkbook workbook;
            if (type == ExcelType.Xlsx) // 2007版本
                workbook = new XSSFWorkbook(excel);
            else // 2003版本
                workbook = new HSSFWorkbook(excel);
            return workbook;
        }

        /// <summary>
        /// 获得值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    return cell.NumericCellValue;
                case CellType.Blank:
                    return cell.StringCellValue;
                case CellType.Formula:
                    return cell.StringCellValue;
                default:
                    return cell.StringCellValue;
            }
        }

        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public class NpoiMemoryStream : MemoryStream
    {
        /// <summary>
        /// 
        /// </summary>
        public NpoiMemoryStream()
        {
            AllowClose = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowClose { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void Close()
        {
            if (AllowClose)
                base.Close();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExcelCell
    {
        public string ShowName { get; set; }

        public string PropertyName { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ExcelType
    {
        Xlsx,
        Xls
    }

}
