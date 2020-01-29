import React from 'react';
import { Container, Row } from 'react-bootstrap';
 
const layout = (props) => {
    return (
        <Container>
            <Row>
                CAMPEONATO DE FILMES
            </Row>
            <main>
                {props.children}
            </main>
        </Container>
    )
}
export default layout;