﻿using LawyerCoreApp.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LawyerCoreApp.Infra.Data.Context;

public class TodoContext : DbContext
{
    #region Constructor


    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {

    }


    #endregion

    #region User
       public DbSet<User> Users { get; set; }
    #endregion

  
 

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs) fk.DeleteBehavior = DeleteBehavior.Restrict;



        #region Seed Data

        #region   User
        //TODO Later
        //modelBuilder.Entity<User>().HasData(new User
        //{
        //});

        #endregion

 

        #endregion

        base.OnModelCreating(modelBuilder);
    }
    #endregion

}