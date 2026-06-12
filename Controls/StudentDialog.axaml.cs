using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using kpk_avalonia.Classes;
using kpk_avalonia.Data;
using System.Linq;

namespace kpk_avalonia;

public partial class StudentDialog : Window
{
    public User User { get; private set; }

    public StudentDialog()
    {
        InitializeComponent();
        User = new User();
        CmbxGroup.ItemsSource = ConnectionClass.connect.Groups.ToList();
    }

    public StudentDialog(User student)
    {
        InitializeComponent();
        User = student;
        TxtFirstName.Text = student.FirstName;
        TxtLastName.Text = student.LastName;
        TxtPatronymic.Text = student.Patronymic;
        CmbxGroup.SelectedItem = ConnectionClass.connect.Groups.Where(z => z.Id == student.GroupId).FirstOrDefault();
        DpBirthday.SelectedDate = student.Dob.HasValue
            ? new DateTimeOffset(student.Dob.Value.ToDateTime(TimeOnly.MinValue))
            : null;

        CmbxGroup.ItemsSource = ConnectionClass.connect.Groups.ToList();
        if (student.Photo != null)
            IPhoto.Source = new Bitmap(new MemoryStream(student.Photo));
    }

    private async void BtnAddImage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Выбрать фото",
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("Изображения") { Patterns = new[] { "*.jpg", "*.jpeg", "*.png", "*.gif" } }
            }
        });

        if (files.Count == 0) return;

        await using var stream = await files[0].OpenReadAsync();
        using var memStream = new MemoryStream();
        await stream.CopyToAsync(memStream);

        User.Photo = memStream.ToArray();

        memStream.Position = 0;
        IPhoto.Source = new Bitmap(memStream);
    }

    private void BtnSave_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        User.FirstName = TxtFirstName.Text;
        User.LastName = TxtLastName.Text;
        User.Patronymic = TxtPatronymic.Text;
        User.Dob = DateOnly.FromDateTime(DpBirthday.SelectedDate.Value.DateTime);
        User.GroupId = ((Group)CmbxGroup.SelectedItem).Id;
        Close(true); // return true = saved
    }

    private void BtnCancel_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close(false); // return false = cancelled
    }

    private void CmbxGroup_SelectionChanged(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}
