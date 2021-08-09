﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pokedex_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

            private void Form1_Load(object sender, EventArgs e)
        {
            //Cargar la grilla con pokemons desde la DB
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                List<Pokemon> listaObtenida = negocio.listar();

                dgvPokemons.DataSource = listaObtenida;
                dgvPokemons.Columns[0].Visible = false;
                dgvPokemons.Columns[4].Visible = false;

                pbxPokemon.Load(listaObtenida[0].UrlImagen);
                lblNombre.Text = listaObtenida[0].Nombre;
                txtDescripcion.Text = listaObtenida[0].Descripcion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon poke = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            pbxPokemon.Load(poke.UrlImagen);

            txtDescripcion.Text = poke.Descripcion;
            lblNombre.Text = poke.Nombre;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Pokemon poke = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;
            string nuevaDescripcion = txtDescripcion.Text;
            int id = poke.Id;
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                if (negocio.actualizar(id, nuevaDescripcion)) {

                    List<Pokemon> listaObtenida = negocio.listar();
                    dgvPokemons.DataSource = null;
                    dgvPokemons.DataSource = listaObtenida;
                    dgvPokemons.Columns[0].Visible = false;
                    dgvPokemons.Columns[4].Visible = false;

                    MessageBox.Show("Descripción actualizada correctamente!");
                } else
                {
                    MessageBox.Show("No se pudo actualizar el pokémon.");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
