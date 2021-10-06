using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimulacaoEmprestimoFGTS.Core.Dto;
using SimulacaoEmprestimoFGTS.Core.Interfaces;
using SimulacaoEmprestimoFGTS.Core.Services;
using SimulacaoEmprestimoFGTS.Domain.Model;
using System;

namespace SimulacaoEmprestimoFGTS.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //var configuration = new ConfigurationBuilder().
            //SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();

            services.AddScoped<IFGTSService, FGTSService>();
            services.AddScoped<IIOFService, IOFService>();
            services.AddScoped<IConversorTaxaService, ConversorTaxaService>();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RepasseFGTS, RepasseFGTSDto>()
                .ForMember(x => x.Aliquota, source => source.MapFrom(src => src.Aliquota.Percentual))
                .ForMember(x => x.ParcelaAdicional, source => source.MapFrom(src => src.Aliquota.ParcelaAdicional));

                cfg.CreateMap<SimulacaoFGTS, SimulacaoFGTSDto>()
                .ForMember(x => x.ValorRepasseFGTS, opt => opt.MapFrom(src => src.Repasse.ValorParcela))
                .ForMember(x => x.DataVencimento, opt => opt.MapFrom(src => src.Repasse.DataVencimento));
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
