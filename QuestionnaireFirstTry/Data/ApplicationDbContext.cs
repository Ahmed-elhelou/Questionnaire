using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using QuestionnaireFirstTry.Models;

namespace QuestionnaireFirstTry.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Question { get; set; }
        public DbSet<ClaimModel> Claims    { get; set; }
        protected override  void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ClaimModel>().HasData(new ClaimModel("View Question"));
            builder.Entity<ClaimModel>().HasData(new ClaimModel("Edit Question"));
            builder.Entity<ClaimModel>().HasData(new ClaimModel("Remove Question"));
            builder.Entity<ClaimModel>().HasData(new ClaimModel("Add Question"));
            
            
        }
    }
}
