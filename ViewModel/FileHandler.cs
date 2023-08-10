using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.UI.Xaml.Media.Imaging;
using OfficeOpenXml;
using OfficeOpenXml.LoadFunctions.Params;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ViewModel;

public static class FileHandler
{


    public static async Task<byte[]> GetBytesFromFileAsync(StorageFile file)
    {
        if (file == null) return new byte[0];

        using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
        {
            var reader = new DataReader(stream);
            await reader.LoadAsync((uint)stream.Size);
            byte[] imageBytes = new byte[stream.Size];
            reader.ReadBytes(imageBytes);
            return imageBytes;
        }
    }

    public static async Task<SoftwareBitmap> GetImageFromBytesAsync(byte[] bytes)
    {
        if (bytes == null || bytes.Length == 0) return null;
        using (IRandomAccessStream stream = bytes.AsBuffer().AsStream().AsRandomAccessStream())
        {
            var Decoder = await BitmapDecoder.CreateAsync(stream);
            var bmp = await Decoder.GetSoftwareBitmapAsync();

            if (bmp.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
                bmp.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                bmp = SoftwareBitmap.Convert(bmp, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }
            return bmp;
        }
    }

    public static async Task<SoftwareBitmapSource> GetImgSourceAsync(byte[] bytes)
    {
        var img = await GetImageFromBytesAsync(bytes);
        var source = await GetImgSourceAsync(img);
        return source;
    }
    public static async Task<SoftwareBitmapSource> GetImgSourceAsync(SoftwareBitmap bitmap)
    {
        if (bitmap == null) return null;
        var source = new SoftwareBitmapSource();
        await source.SetBitmapAsync(bitmap);
        return source;
    }

    public static async Task<PdfDocument> GetPdfAsync(byte[] bytes)
    {
        using (IRandomAccessStream stream = bytes.AsBuffer().AsStream().AsRandomAccessStream())
        {
            PdfDocument pdfDocument = await PdfDocument.LoadFromStreamAsync(stream);
            return pdfDocument;
        }
    }

    public static async Task SaveToExcel(string FilePath, IEnumerable<object> Data, string PageName = "", MemberInfo[] members = null)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var Package = new ExcelPackage(FilePath);
        try
        {
            ExcelWorksheet worksheet;
            var WorkSheetNames = Package.Workbook.Worksheets.Select(x => x.Name).ToList();
            StringBuilder FinalName = new(PageName);
            int i = 1;
            while (WorkSheetNames.Contains(FinalName.ToString()))
            {
                FinalName.Clear();
                FinalName.Append(PageName + $" {i}");
                i++;
            }


            worksheet = Package.Workbook.Worksheets.Add(FinalName.ToString());


            worksheet.Cells["A1"].LoadFromCollection(Data, true, TableStyles.Light1,
                LoadFromCollectionParams.DefaultBindingFlags, members).AutoFitColumns();
            await Package.SaveAsync();

        }
        catch
        {
            await SaveToExcel(FilePath, Data, PageName + " New");
        }



    }

}
