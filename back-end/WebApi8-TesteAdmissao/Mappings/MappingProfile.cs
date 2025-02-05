using AutoMapper;
using WebApi8_TesteAdmissao.Models;
using WebApi8_TesteAdmissao.Dto.Pessoa;
using WebApi8_TesteAdmissao.Dto.Endereco;

namespace WebApi8_TesteAdmissao.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PessoaModel, PessoaCriacaoDto>()
            .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco)).ReverseMap();

            CreateMap<EditaPessoaDto, PessoaModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<EnderecoDto, EnderecoModel>().ReverseMap();
        }
    }
}
