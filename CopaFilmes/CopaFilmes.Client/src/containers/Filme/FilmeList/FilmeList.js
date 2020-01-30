import React, { Component } from 'react';
import { Jumbotron } from 'react-bootstrap';
import Aux from '../../../hoc/Auxiliary/Auxiliary';

class FilmeList extends Component {
    render() {
        return (
            <Aux>
                <Jumbotron>
                    <h5 class="text-center">CAMPEONATO DE FILMES</h5>
                    <h1 class="text-center"><strong>Resultado Final</strong></h1>
                    <hr />
                    <p class="text-center">Veja o resultado final do Campeonato de filmes de forma simples e rápida</p>
                </Jumbotron>
            </Aux>
        )
    }
}

export default FilmeList;