using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda.Microservicios.Autor.Api.Modelo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class ConsultarAutorNombre
    {
        public class AutorPorNombre : IRequest<List<AutorDto>>
        {
            public string Nombre { get; set; }
        }

        public class Manejador : IRequestHandler<AutorPorNombre, List<AutorDto>>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(AutorPorNombre request, CancellationToken cancellationToken)
            {
                var autores = await _context.AutorLibros
                    .Where(p => p.Nombre.Contains(request.Nombre))
                    .ToListAsync();

                if (autores == null || !autores.Any())
                {
                    return new List<AutorDto>(); // Retornar lista vacía en lugar de lanzar excepción
                }

                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                return autoresDto;
            }
        }
    }
}