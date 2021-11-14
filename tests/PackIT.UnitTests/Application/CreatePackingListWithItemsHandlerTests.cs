using System;
using System.Threading.Tasks;
using NSubstitute;
using PackIT.Application.Commands;
using PackIT.Application.Commands.Handlers;
using PackIT.Application.DTO.External;
using PackIT.Application.Exceptions;
using PackIT.Application.Services;
using PackIT.Domain.Consts;
using PackIT.Domain.Entities;
using PackIT.Domain.Factories;
using PackIT.Domain.Repositories;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Commands;
using Shouldly;
using Xunit;

namespace PackIT.UnitTests.Application
{
    public class CreatePackingListWithItemsHandlerTests
    {
        Task Act(CreatePackingListWithItems command)
            => _commandHandler.HandleAsync(command);

        [Fact]
        public async Task HandleAsync_Throws_PackingListAlreadyExistsException_When_List_With_same_Name_Already_Exists()
        {
            var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Female, default);
            _readService.ExistsByNameAsync(command.Name).Returns(true);

            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<PackingListAlreadyExistsException>();
        }

        [Fact]
        public async Task HandleAsync_Throws_MissingLocalizationWeatherException_When_Weather_Is_Not_Returned_From_Service()
        {
            var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Female,
                new LocalizationWriteModel("Warsaw", "Poland"));
            
            _readService.ExistsByNameAsync(command.Name).Returns(false);
            _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(default(WeatherDto));
            
            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<MissingLocalizationWeatherException>();
        }
        
        [Fact]
        public async Task HandleAsync_Calls_Repository_On_Success()
        {
            var command = new CreatePackingListWithItems(Guid.NewGuid(), "MyList", 10, Gender.Female,
                new LocalizationWriteModel("Warsaw", "Poland"));
            
            _readService.ExistsByNameAsync(command.Name).Returns(false);
            _weatherService.GetWeatherAsync(Arg.Any<Localization>()).Returns(new WeatherDto(12));
            _factory.CreateWithDefaultItems(command.Id, command.Name, command.Days, command.Gender,
                Arg.Any<Temperature>(), Arg.Any<Localization>()).Returns(default(PackingList));
            
            var exception = await Record.ExceptionAsync(() => Act(command));

            exception.ShouldBeNull();
            await _repository.Received(1).AddAsync(Arg.Any<PackingList>());
        }

        #region ARRANGE

        private readonly ICommandHandler<CreatePackingListWithItems> _commandHandler;
        private readonly IPackingListRepository _repository;
        private readonly IWeatherService _weatherService;
        private readonly IPackingListReadService _readService;
        private readonly IPackingListFactory _factory;
        
        public CreatePackingListWithItemsHandlerTests()
        {
            _repository = Substitute.For<IPackingListRepository>();
            _weatherService = Substitute.For<IWeatherService>();
            _readService = Substitute.For<IPackingListReadService>();
            _factory = Substitute.For<IPackingListFactory>();
            
            _commandHandler = new CreatePackingListWithItemsHandler(_repository, _factory, _readService, _weatherService);
        }

        #endregion
    }
}