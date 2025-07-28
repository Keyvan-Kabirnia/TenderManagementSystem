using AutoMapper;
using Tms.Application.DTOs.Auth;
using Tms.Application.DTOs.Bid;
using Tms.Application.DTOs.Lookup;
using Tms.Application.DTOs.Tender;
using Tms.Application.DTOs.Vendor;
using Tms.Application.Tenders.Requests;
using Tms.Application.Vendors.Requests;
using Tms.Domain.Entities;

namespace Tms.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Auth mappings
        CreateMap<UserEntity, LoginResponseDto>()
            .ForMember(dest => dest.Token, opt => opt.Ignore())
            .ForMember(dest => dest.ExpiresAt, opt => opt.Ignore());

        // Tender mappings
        CreateMap<TenderEntity, TenderDto>();
        CreateMap<TenderEntity, TenderDetailDto>();
        CreateMap<Tenders.Requests.CreateTenderRequest, TenderEntity>();
        CreateMap<Tenders.Requests.UpdateTenderRequest, TenderEntity>();

        // Vendor mappings
        CreateMap<VendorEntity, VendorDto>();
        CreateMap<VendorEntity, VendorDetailDto>();
        CreateMap<Vendors.Requests.CreateVendorRequest, VendorEntity>();

        // Bid mappings
        CreateMap<BidEntity, BidDto>();

        // Lookup mappings
        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<StatusEntity, StatusDto>();

        // Vendor bid mapping
        CreateMap<BidEntity, VendorBidDto>()
            .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender.Title));
    }
}