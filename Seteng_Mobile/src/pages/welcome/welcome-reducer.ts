import { WELCOME_ACTION } from './welcome-action';

export interface IWelcomeState {
    item?: User,
  
}

const INIT_STATE: IWelcomeState = {
    item: {} as User,  
  
};

export function WelcomeReducer(state = INIT_STATE, action: any): IWelcomeState {

    switch (action.type) {
        case WELCOME_ACTION.GET:
            return {
                item: action.payload,             
            }    
        default:
            return state;
    }
}