using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Api.Controllers;
using TaskTracker.Api.Dtos;
using TaskTracker.Api.Repositories;
using TaskTracker.Models.Models;



namespace TaskTracker.Tests.Controllers
{
    public class TasksControllerTests
    {
        
        [Fact]
        
        public async Task CreateTask_ShouldReturnCreated_WhenTaskIsValid()
        {
         
            var repoMock = new Mock<ITaskRepository>();
            var mapperMock = new Mock<IMapper>();

          
            mapperMock.Setup(m => m.Map<TaskItem>(It.IsAny<TaskItemDto>()))
                      .Returns((TaskItemDto dto) => new TaskItem
                      {

                          TaskId = 0,
                          UserId = dto.UserId,
                          Title = dto.Title,
                          Description = dto.Description,
                          Status = dto.Status,
                          CreatedAt = dto.CreatedAt
                      });

            repoMock.Setup(r => r.CreateTask(It.IsAny<TaskItem>()))
                    .ReturnsAsync(1);

            var controller = new TaskController(repoMock.Object, mapperMock.Object);

            var dto = new TaskItemDto
            {
                UserId = 1,
                Title = "Valid Task",
                Description = "Test",
                Status = 0
            };

       
            var result = await controller.CreateTask(dto);

          
            var created = result as CreatedAtActionResult;
            created.Should().NotBeNull();
            created!.RouteValues!["id"].Should().Be(1);
            var returnedDto = created.Value as TaskItemDto;
            returnedDto!.TaskId.Should().Be(1);
        }

        [Fact]
        public async Task CreateTask_ShouldReturnBadRequest_WhenTitleIsEmpty()
        {
          
            var repoMock = new Mock<ITaskRepository>();
            var mapperMock = new Mock<IMapper>();

            var controller = new TaskController(repoMock.Object, mapperMock.Object);

            var dto = new TaskItemDto
            {
                UserId = 1,
                Title = "", 
                Description = "Test",
                Status = 0
            };

         
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                controller.ModelState.AddModelError("Title", "Title is required");
            }

         
            var result = await controller.CreateTask(dto);

         
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task CreateTask_ShouldReturn500_WhenRepositoryThrows()
        {
          
            var repoMock = new Mock<ITaskRepository>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<TaskItem>(It.IsAny<TaskItemDto>()))
                      .Returns(new TaskItem());

            repoMock.Setup(r => r.CreateTask(It.IsAny<TaskItem>()))
                    .ThrowsAsync(new Exception("DB Error"));

            var controller = new TaskController(repoMock.Object, mapperMock.Object);

            var dto = new TaskItemDto
            {
                UserId = 1,
                Title = "Some Task",
                Description = "Test",
                Status = 0
            };

       
            Func<Task> act = async () => await controller.CreateTask(dto);

         
            await act.Should().ThrowAsync<Exception>().WithMessage("DB Error");
        }
    }
}