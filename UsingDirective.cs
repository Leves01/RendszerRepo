global using System.Text;

//Models
global using RendszerRepo.Models;

//Dtos
global using RendszerRepo.Dtos.User;
global using RendszerRepo.Dtos.Part;

//Mapper
global using AutoMapper;

//Services
global using RendszerRepo.Services.UserService;
global using RendszerRepo.Services.PartService;

global using Microsoft.EntityFrameworkCore;
global using RendszerRepo.Data;
global using RendszerRepo.Services.StorageService;

//Login and Authorize
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.OpenApi.Models;
global using Swashbuckle.AspNetCore.Filters;

//Database
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
