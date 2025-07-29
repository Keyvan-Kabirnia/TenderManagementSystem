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
        CreateMap<CreateTenderRequest, TenderEntity>();
        CreateMap<UpdateTenderRequest, TenderEntity>();

        // Vendor mappings
        CreateMap<VendorEntity, VendorDto>()
            .ForMember(dest=> dest.Email, opt => opt.MapFrom(src => src.User.Email));
        CreateMap<VendorEntity, VendorDetailDto>();
        CreateMap<CreateVendorRequest, VendorEntity>();

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