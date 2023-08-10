using Backend.Collections;
using Backend.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.VM_Models
{
    public partial class CatagoryVM : ObservableObject
    {
        [ObservableProperty]
        int catagoryId;

        [ObservableProperty]
        string catagoryName;


        public override string ToString()
        {
            return this.CatagoryName ?? string.Empty;
        }

        public CatagoryVM()
        {
            catagoryId = 0;
            catagoryName = string.Empty;

        }

        public CatagoryVM(Catagory catagory)
        {
            catagoryId = catagory.CatagoryId;
            catagoryName = catagory.CatagoryName;
        }

        public void FromDTO(Catagory catagory)
        {
            CatagoryId = catagory.CatagoryId;
            CatagoryName = catagory.CatagoryName;

        }

        public Catagory ToDTO()
        {
            var e = new CustomCollection<Employee>();

            return new()
            {
                CatagoryId = this.CatagoryId,
                CatagoryName = this.CatagoryName
            };
        }
    }
}
