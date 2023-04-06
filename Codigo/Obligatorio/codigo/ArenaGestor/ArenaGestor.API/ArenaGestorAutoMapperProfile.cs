using ArenaGestor.APIContracts.Artist;
using ArenaGestor.APIContracts.Band;
using ArenaGestor.APIContracts.Concert;
using ArenaGestor.APIContracts.Country;
using ArenaGestor.APIContracts.Gender;
using ArenaGestor.APIContracts.Roles;
using ArenaGestor.APIContracts.Security;
using ArenaGestor.APIContracts.Soloist;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.APIContracts.Users;
using ArenaGestor.Domain;
using ArenaGestor.Extensions.DTO;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGestor.API
{
    public class ArenaGestorAutoMapperProfile : Profile
    {
        public ArenaGestorAutoMapperProfile()
        {
            CreateMap<ArtistGetArtistsDto, Artist>();
            CreateMap<ArtistInsertArtistDto, Artist>();
            CreateMap<Artist, ArtistResultArtistDto>()
                .ForMember(x => x.UserId, y => y.MapFrom(z => z.User.UserId));
            CreateMap<List<Artist>, IEnumerable<ArtistResultArtistDto>>();
            CreateMap<ArtistUpdateArtistDto, Artist>();

            CreateMap<BandGetArtistsDto, Artist>();
            CreateMap<BandGetBandsDto, Band>();
            CreateMap<BandInsertArtistDto, ArtistBand>();
            CreateMap<BandInsertBandDto, Band>();
            CreateMap<ArtistBand, BandResultArtistDto>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Artist.Name));
            CreateMap<Artist, BandResultArtistDto>();
            CreateMap<Band, BandResultBandDto>();
            CreateMap<List<Band>, IEnumerable<BandResultBandDto>>();
            CreateMap<Gender, BandResultGenderDto>();
            CreateMap<Concert, BandResultConcertDto>();
            CreateMap<BandUpdateArtistDto, ArtistBand>();
            CreateMap<BandUpdateBandDto, Band>();
            CreateMap<ConcertProtagonist, BandResultConcertDto>()
                .ForMember(dto => dto.TourName, x => x.MapFrom(prot => prot.Concert.TourName))
                .ForMember(dto => dto.Date, x => x.MapFrom(prot => prot.Concert.Date))
                .ForMember(dto => dto.Price, x => x.MapFrom(prot => prot.Concert.Price))
                .ForMember(dto => dto.TicketCount, x => x.MapFrom(prot => prot.Concert.TicketCount))
                .ForMember(dto => dto.Location, x => x.MapFrom(prot => prot.Concert.Location));

            CreateMap<RoleArtist, BandResultRoleArtistDto>();
            CreateMap<List<RoleArtist>, IEnumerable<BandResultRoleArtistDto>>();

            CreateMap<SoloistInsertRoleArtistDto, RoleArtist>();
            CreateMap<List<SoloistInsertRoleArtistDto>, IEnumerable<RoleArtist>>();
            CreateMap<SoloistUpdateRoleArtistDto, RoleArtist>();
            CreateMap<List<SoloistUpdateRoleArtistDto>, IEnumerable<RoleArtist>>();
            CreateMap<RoleArtist, SoloistResultRoleArtistDto>();
            CreateMap<List<RoleArtist>, IEnumerable<SoloistResultRoleArtistDto>>();

            CreateMap<GenderGetGendersDto, Gender>();
            CreateMap<GenderInsertGenderDto, Gender>();
            CreateMap<Gender, GenderResultGenderDto>();
            CreateMap<List<Gender>, IEnumerable<GenderResultGenderDto>>();
            CreateMap<GenderUpdateGenderDto, Gender>();

            CreateMap<SoloistGetArtistsDto, Artist>();
            CreateMap<SoloistGetSoloistsDto, Soloist>();
            CreateMap<SoloistInsertSoloistDto, Soloist>();
            CreateMap<Artist, SoloistResultArtistDto>();
            CreateMap<Soloist, SoloistResultSoloistDto>();
            CreateMap<List<Soloist>, IEnumerable<SoloistResultSoloistDto>>();
            CreateMap<Gender, SoloistResultGenderDto>();
            CreateMap<ConcertProtagonist, SoloistResultConcertDto>()
                .ForMember(dto => dto.TourName, x => x.MapFrom(prot => prot.Concert.TourName))
                .ForMember(dto => dto.Date, x => x.MapFrom(prot => prot.Concert.Date))
                .ForMember(dto => dto.Price, x => x.MapFrom(prot => prot.Concert.Price))
                .ForMember(dto => dto.TicketCount, x => x.MapFrom(prot => prot.Concert.TicketCount))
                .ForMember(dto => dto.Location, x => x.MapFrom(prot => prot.Concert.Location));

            CreateMap<SoloistUpdateSoloistDto, Soloist>();

            CreateMap<UserGetUsersDto, User>();
            CreateMap<UserInsertUserDto, User>();
            CreateMap<User, UserResultUserDto>();
            CreateMap<UserUpdateUserDto, User>();
            CreateMap<UserChangePasswordDto, UserChangePassword>();
            CreateMap<UserRoleDto, UserRole>();
            CreateMap<List<UserRoleDto>, IEnumerable<UserRole>>();
            CreateMap<UserRole, UserRoleDto>().ForMember(x => x.Name, y => y.MapFrom(y => y.Role.Name));
            CreateMap<List<UserRole>, IEnumerable<UserRoleDto>>();

            CreateMap<ConcertGetConcertsDto, ConcertFilter>();
            CreateMap<ConcertGetDateRangeConcertsDto, DateRange>();

            CreateMap<ConcertInsertConcertDto, Concert>();
            CreateMap<ConcertInsertProtagonistDto, ConcertProtagonist>();
            CreateMap<ConcertUpdateConcertDto, Concert>();
            CreateMap<ConcertUpdateProtagonistDto, ConcertProtagonist>();

            CreateMap<Location, ConcertResultLocationDto>();
            CreateMap<Country, ConcertResultCountryDto>();
            CreateMap<ConcertInsertCountryDto, Country>();
            CreateMap<ConcertInsertLocationDto, Location>();
            CreateMap<Location, BandResultLocationDto>();
            CreateMap<Country, BandResultCountryDto>();
            CreateMap<Location, SoloistResultLocationDto>();
            CreateMap<Country, SoloistResultCountryDto>();

            CreateMap<Concert, ConcertResultConcertDto>();
            CreateMap<Concert, ConcertResultConcertArtistDto>()
                .ForMember(dto => dto.TicketSold, x => x.MapFrom(prot => (prot.Tickets.Sum(x => x.Amount))));
            CreateMap<ConcertProtagonist, ConcertGetMusicalProtagonistDto>()
                .ForMember(dto => dto.Name, x => x.MapFrom(prot => prot.Protagonist.Name));

            CreateMap<TicketStatus, TicketStatusDto>();
            CreateMap<Ticket, TicketSellTicketResultDto>();
            CreateMap<Ticket, TicketBuyTicketResultDto>();
            CreateMap<Ticket, TicketScanTicketResultDto>();
            CreateMap<TicketSellTicketDto, TicketSell>();
            CreateMap<TicketBuyTicketDto, TicketBuy>();
            CreateMap<Ticket, TicketGetTicketResultDto>();
            CreateMap<Concert, TicketConcertDto>();
            CreateMap<List<Ticket>, IEnumerable<TicketGetTicketResultDto>>();

            CreateMap<User, SecurityLoggedUser>();
            CreateMap<UserRole, SecurityUserRole>().ForMember(x => x.Name, y => y.MapFrom(y => y.Role.Name));

            CreateMap<ConcertDto, Concert>();
            CreateMap<IEnumerable<ConcertProtagonistDto>, List<ConcertProtagonist>>();
            CreateMap<ConcertProtagonistDto, ConcertProtagonist>();
            CreateMap<Role, RolesResultDto>();
            CreateMap<RoleArtist, RolesArtistResultDto>();

            CreateMap<List<Concert>, IEnumerable<ConcertDto>>();
            CreateMap<Concert, ConcertDto>();
            CreateMap<Gender, GenderDto>();
            CreateMap<MusicalProtagonist, ProtagonistDto>();
            CreateMap<ProtagonistDto, MusicalProtagonist>();
            CreateMap<ConcertProtagonist, ConcertProtagonistDto>();

            CreateMap<ArtistInsertUserDto, User>();
            CreateMap<ArtistUpdateUserDto, User>();
            CreateMap<Country, CountryResultDto>();
            CreateMap<ConcertUpdateCountryDto, Country>();
            CreateMap<ConcertUpdateLocationDto, Location>();
        }
    }
}
