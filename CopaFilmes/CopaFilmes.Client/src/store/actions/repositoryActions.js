import * as actionTypes from './actionTypes';
import axios from '../../axios/axios';

const getDataSuccess = (data) => {
    return {
        type: actionTypes.GET_DATA_SUCCESS,
        data: data
    }
}

export const getData = (url, props) => {
    return (dispatch) => {
        axios.get(url)
            .then(response => {
                dispatch(getDataSuccess(response.data));
            })
            .catch(error => {
                //TODO: handle the error when implemented
            })
    }
}