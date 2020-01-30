import * as actionTypes from '../actions/actionTypes';

const initialState = {
    data: null,
    showSuccessModal: false
}
	
const executeGetDataSuccess = (state, action) => {
    return {
        ...state,
        data: action.data
    }
}

const reducer = (state = initialState, action) => {
    if (action.type === actionTypes.GET_DATA_SUCCESS) {
        return executeGetDataSuccess(state, action);
    }

    return state;
}

export default reducer;