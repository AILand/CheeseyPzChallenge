using Application.Core;
using Domain.Data.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Cheeses.Commands
{
    //TODO: Add more validation and error logging
    public class UpsertCheeseCommandHandler : IUseCaseHandler<UpsertCheeseCommand, Guid>
    {
        private readonly ILogger<UpsertCheeseCommandHandler> logger;
        private readonly ICheeseRepository cheeseRepository;

        public UpsertCheeseCommandHandler(ILogger<UpsertCheeseCommandHandler> logger, ICheeseRepository cheeseRepository)
        {
            this.logger=logger;
            this.cheeseRepository=cheeseRepository;
        }

        public async Task<Guid> HandleAsync(UpsertCheeseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return await Insert(request, cancellationToken);
            }
            await Update(request, cancellationToken);

            return new Guid(request.Id);
        }

        private async Task Update(UpsertCheeseCommand request, CancellationToken cancellationToken)
        {
            Guid guidId;
            bool isValidId = Guid.TryParse(request.Id, out guidId);

            if (!isValidId)
            {
                throw new ArgumentOutOfRangeException(nameof(request.Id));
            }

            var cheese = this.cheeseRepository.GetCheeseByID(guidId);
            cheese.Name = request.Name;
            cheese.PricePerKg = (int)(request.PricePerKg*100);
            cheese.Color = request.Color;
            cheese.Image = ConvertFormFileToBase64(request.Image);

            this.cheeseRepository.Update(cheese);
        }

        private async Task<Guid> Insert(UpsertCheeseCommand request, CancellationToken cancellationToken)
        {

            if ((request.Image?.Length??0) > 0)
            {
                using (var ms = new MemoryStream())
                {
                    request.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                }
            }

            var cheese = new Cheese
            {
                Name = request.Name,
                PricePerKg=(int)(request.PricePerKg*100),
                Color = request.Color,
                Image = ConvertFormFileToBase64(request.Image),
            };

            this.cheeseRepository.Insert(cheese);
            return cheese.Id;
        }


        public byte[] ConvertFormFileToBase64(IFormFile file)
        {
            byte[] fileData = new byte[0];

            if ((file?.Length??0) > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileData = ms.ToArray();
                }
            }
            return fileData;
        }
    }
}

