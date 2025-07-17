using InventoryManagement.RepositoryLayer;
using InventoryManagement.CommonLayer.Models;
using Azure.Core;

namespace InventoryManagement.ServiceLayer
{
    public class INVApplicationSL : IINVApplicationSL
    {
        public readonly IINVApplicationRL _invApplicationRL;
        public INVApplicationSL(IINVApplicationRL invApplicationRL)
        {
            _invApplicationRL = invApplicationRL;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await _invApplicationRL.Login(request);
        }
        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            return await _invApplicationRL.Register(request);
        }




        public async Task<CreateInformationResponse> CreateInformation(IItemCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }
        public async Task<CreateInformationResponse> CreateInformation(ItemTransCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }

        public async Task<CreateInformationResponse> CreateInformation(GDCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }
        public async Task<CreateInformationResponse> CreateInformation(SupplierInfoCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }
        public async Task<CreateInformationResponse> CreateInformation(ItemCatCreateInfoCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }
        public async Task<CreateInformationResponse> CreateInformation(SubCatCreateInfoCreateInformationRequest request)
        {

            return await _invApplicationRL.CreateInformation(request);

        }




        public async Task<ReadInformationResponse> ItemReadInformation()
        {

            return await _invApplicationRL.ItemReadInformation();

        }
        public async Task<ReadInformationResponse> ICReadInformation()
        {

            return await _invApplicationRL.ICReadInformation();

        }
        public async Task<ReadInformationResponse> ItemTransReadInformation()
        {

            return await _invApplicationRL.ItemTransReadInformation();

        }
        public async Task<ReadInformationResponse> ITCReadInformation()
        {

            return await _invApplicationRL.ITCReadInformation();

        }
        public async Task<ReadInformationResponse> GDReadInformation()
        {

            return await _invApplicationRL.GDReadInformation();

        }
        public async Task<ReadInformationResponse> GDCReadInformation()
        {

            return await _invApplicationRL.GDCReadInformation();

        }
        public async Task<ReadInformationResponse> SupCReadInformation()
        {

            return await _invApplicationRL.SupCReadInformation();

        }
        public async Task<ReadInformationResponse> ItemCatCReadInformation()
        {

            return await _invApplicationRL.ItemCatCReadInformation();

        }
        public async Task<ReadInformationResponse> SubCatCReadInformation()
        {

            return await _invApplicationRL.SubCatCReadInformation(); 
        }

        public async Task<ReadInformationResponse> SubCatInformation()
        {

            return await _invApplicationRL.SubCatInformation();
        }

        public async Task<ReadInformationResponse> SupplierReadInformation()
        {

            return await _invApplicationRL.SupplierReadInformation();

        }
        public async Task<ReadInformationResponse> ItemCatInformation()
        {

            return await _invApplicationRL.ItemCatInformation();

        }





        public async Task<UpdateInformationResponse> UpdateInformation(ItemUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }
        public async Task<UpdateInformationResponse> UpdateInformation(ItemTransUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }
        public async Task<UpdateInformationResponse> UpdateInformation(GDUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }
        public async Task<UpdateInformationResponse> UpdateInformation(SupplierUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }
        public async Task<UpdateInformationResponse> UpdateInformation(ItemCatUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }

        public async Task<UpdateInformationResponse> UpdateInformation(SubCatUpdateInformationRequest request)
        {

            return await _invApplicationRL.UpdateInformation(request);

        }




        public async Task<SearchInformationByNameResponse> ItemSearchInformationByCat(SearchInformationByNameRequest request)
        {

            return await _invApplicationRL.ItemSearchInformationByCat(request);

        }


        public async Task<SearchInformationByNameResponse> SubCatSearchInformationByCat(SearchInformationByNameRequest request)
        {

            return await _invApplicationRL.SubCatSearchInformationByCat(request);

        }




        public async Task<DeleteInformationResponse> IItemDeleteInfo(DeleteInformationRequest request)
        {

            return await _invApplicationRL.IItemDeleteInfo(request);

        }
        public async Task<DeleteInformationResponse> GDDeleteInfo(DeleteInformationRequest request)
        {

            return await _invApplicationRL.GDDeleteInfo(request);

        }
        public async Task<DeleteInformationResponse> SupplierDeleteInfo(DeleteInformationRequest request)
        {

            return await _invApplicationRL.SupplierDeleteInfo(request);

        }
        public async Task<DeleteInformationResponse> ItemCatDeleteInfo(DeleteInformationRequest request)
        {

            return await _invApplicationRL.ItemCatDeleteInfo(request);

        }
        public async Task<DeleteInformationResponse> SubCatDeleteInfo(DeleteInformationRequest request)
        {

            return await _invApplicationRL.SubCatDeleteInfo(request);

        }

    }
}
