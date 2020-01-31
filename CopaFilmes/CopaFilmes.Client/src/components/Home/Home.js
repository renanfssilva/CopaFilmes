import React, { Component } from 'react';
import './Home.css'
import { Button, Col, Row, Jumbotron } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Aux from '../../hoc/Auxiliary/Auxiliary';
import Filme from '../../components/FilmeComponents/Filme/Filme';

import { connect } from 'react-redux';
import * as repositoryActions from '../../store/actions/repositoryActions';

class Home extends Component {
    componentDidMount = () => {
        let url = '/api/filmes';
        this.props.onGetData(url, { ...this.props });
    }

    state = {
        filmesSelecionados: {},
        isFormValid: false
    }

    render() {
        let filmes = [];

        if (this.props.data && this.props.data.length > 0) {
            filmes = this.props.data.map((filme) => {
                return (
                    <Filme key={filme.id} filme={filme} {...this.props} />
                )
            })
        }

        return (
            <Aux>
                <Jumbotron>
                    <h5 className={'text-center'}>CAMPEONATO DE FILMES</h5>
                    <h1 className={'text-center'}><strong>Fase de Seleção</strong></h1>
                    <hr />
                    <p className={'text-center'}>Selecione 8 filmes que você deseja que entrem na competição e depois pressione o botão Gerar Meu Campeonato para prosseguir.</p>
                </Jumbotron>
                <Row>
                    <Col sm={8}>
                        <strong>
                            <p className={'selecionados'}>
                                Selecionados<br />
                                0 de 8 filmes
                            </p>
                        </strong>
                    </Col>
                    <Col sm={4} className={'col-botao-gera'}>
                        <LinkContainer to='/resultado' className={'ml-auto'}>
                            <Button type='submit' disabled={!this.state.isFormValid} style={{ width: '100%' }}>
                                GERAR MEU CAMPEONATO
                            </Button>
                        </LinkContainer>
                    </Col>
                </Row>
                <br />
                <Row>
                    {filmes}
                </Row>
            </Aux>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        data: state.data
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onGetData: (url, props) => dispatch(repositoryActions.getData(url, props))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);