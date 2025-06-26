﻿using FluentValidation;
using MediatR;
using Tienda.Microservicios.Autor.Api.Modelo;
using Tienda.Microservicios.Autor.Api.Persistencia;

namespace Tienda.Microservicios.Autor.Api.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(p => p.Nombre).NotEmpty();
                RuleFor(p => p.Apellido).NotEmpty();
                RuleFor(p => p.FechaNacimiento).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _context;
            public Manejador(ContextoAutor context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken){
                    var autorLibro = new AutorLibro
                    {
                        Nombre = request.Nombre,
                        Apellido = request.Apellido,
                        FechaNacimiento = request.FechaNacimiento,
                        AutorLibroGuid = Convert.ToString(Guid.NewGuid()) // Generar un GUID único
                    };

                    _context.AutorLibros.Add(autorLibro); // Agregar el nuevo autor al contexto
                var respuesta = await _context.SaveChangesAsync();
                    if (respuesta > 0)
                    {
                        return Unit.Value; // Retorna un valor vacío si la operación fue exitosa
                    }
                    throw new Exception("No se pudo insertar el autor del libro");
                }

            }
        }
    }