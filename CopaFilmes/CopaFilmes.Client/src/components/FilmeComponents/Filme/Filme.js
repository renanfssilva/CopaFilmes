import React from 'react';
import './Filme.css'
import Aux from '../../../hoc/Auxiliary/Auxiliary';
import { Card, Form } from 'react-bootstrap';

const filme = (props) => {
    return (
        <Aux>
            <Card className={'p-3 filme-card'} border="primary">
                <Card.Body>
                    <Card.Title>
                        {['checkbox'].map(type => (
                            <div key={`custom-${props.filme.id}`} className="mb-3">
                                <strong>
                                    <Form.Check
                                        custom
                                        type={type}
                                        id={`custom-${props.filme.id}`}
                                        label={props.filme.titulo}
                                    />
                                </strong>
                            </div>
                        ))}
                    </Card.Title>
                    <Card.Text className={'tab-space text-muted'}>{props.filme.ano}</Card.Text>
                </Card.Body>
            </Card>
        </Aux>
    )
}

export default filme;