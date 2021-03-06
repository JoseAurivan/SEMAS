// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220105124245_newCesta")]
    partial class newCesta
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Auditoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LogEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MessageTemplate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("auditoria");
                });

            modelBuilder.Entity("Domain.Models.CadastroCmas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Beneficio")
                        .HasColumnType("bit");

                    b.Property<string>("Familia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Inseguranca")
                        .HasColumnType("bit");

                    b.Property<string>("Localidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nis")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Residencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sanitizacao")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Nis")
                        .IsUnique();

                    b.ToTable("cadastroCMAS");
                });

            modelBuilder.Entity("Domain.Models.Certificado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CopiaPdf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurriculoId");

                    b.ToTable("Certificado");
                });

            modelBuilder.Entity("Domain.Models.CestaBasica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caminhos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Demandas")
                        .HasColumnType("int");

                    b.Property<bool?>("DeterminacaoJuridica")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroMeses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quant")
                        .HasColumnType("int");

                    b.Property<bool?>("RecomendacaoTecnica")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("cestaBasica");
                });

            modelBuilder.Entity("Domain.Models.Curriculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Competencias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resumo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Curriculo");
                });

            modelBuilder.Entity("Domain.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cep")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CestaId")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Complemento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoEndereco")
                        .HasColumnType("int");

                    b.Property<int>("TipoResidencia")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CestaId");

                    b.ToTable("enderecos");
                });

            modelBuilder.Entity("Domain.Models.Entrega", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CestaBasicaId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataEntrega")
                        .HasColumnType("datetime2");

                    b.Property<int>("EntregaStatus")
                        .HasColumnType("int");

                    b.Property<string>("NomeAgente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Unidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CestaBasicaId");

                    b.ToTable("entrega");
                });

            modelBuilder.Entity("Domain.Models.Experiencias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CurriculoId");

                    b.ToTable("Experiencias");
                });

            modelBuilder.Entity("Domain.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CadastroCmasId")
                        .HasColumnType("int");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CurriculoId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CadastroCmasId");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("CurriculoId");

                    b.ToTable("pessoa");
                });

            modelBuilder.Entity("Domain.Models.PessoaEndereco", b =>
                {
                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.HasKey("PessoaId", "EnderecoId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("PessoaEndereco");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Roles")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("user");
                });

            modelBuilder.Entity("Domain.Models.Certificado", b =>
                {
                    b.HasOne("Domain.Models.Curriculo", "Curriculo")
                        .WithMany("Certificados")
                        .HasForeignKey("CurriculoId");

                    b.Navigation("Curriculo");
                });

            modelBuilder.Entity("Domain.Models.Endereco", b =>
                {
                    b.HasOne("Domain.Models.CestaBasica", "Cesta")
                        .WithMany()
                        .HasForeignKey("CestaId");

                    b.Navigation("Cesta");
                });

            modelBuilder.Entity("Domain.Models.Entrega", b =>
                {
                    b.HasOne("Domain.Models.CestaBasica", "CestaBasica")
                        .WithMany("Entregas")
                        .HasForeignKey("CestaBasicaId");

                    b.Navigation("CestaBasica");
                });

            modelBuilder.Entity("Domain.Models.Experiencias", b =>
                {
                    b.HasOne("Domain.Models.Curriculo", "Curriculo")
                        .WithMany("Experiencias")
                        .HasForeignKey("CurriculoId");

                    b.Navigation("Curriculo");
                });

            modelBuilder.Entity("Domain.Models.Pessoa", b =>
                {
                    b.HasOne("Domain.Models.CadastroCmas", "CadastroCmas")
                        .WithMany()
                        .HasForeignKey("CadastroCmasId");

                    b.HasOne("Domain.Models.Curriculo", "Curriculo")
                        .WithMany()
                        .HasForeignKey("CurriculoId");

                    b.Navigation("CadastroCmas");

                    b.Navigation("Curriculo");
                });

            modelBuilder.Entity("Domain.Models.PessoaEndereco", b =>
                {
                    b.HasOne("Domain.Models.Endereco", "Endereco")
                        .WithMany("Pessoa")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Pessoa", "Pessoa")
                        .WithMany("Enderecos")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Domain.Models.CestaBasica", b =>
                {
                    b.Navigation("Entregas");
                });

            modelBuilder.Entity("Domain.Models.Curriculo", b =>
                {
                    b.Navigation("Certificados");

                    b.Navigation("Experiencias");
                });

            modelBuilder.Entity("Domain.Models.Endereco", b =>
                {
                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Domain.Models.Pessoa", b =>
                {
                    b.Navigation("Enderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
