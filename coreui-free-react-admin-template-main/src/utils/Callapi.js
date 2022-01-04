import axios from 'axios';
import * as Config from './Config';


export default function Callapi(endpoint,method='GET',body){
    return axios({
        method: method,
        url:` ${Config.API_URL}/${endpoint}`,
        data: body,
      }).catch((error) => {

        });

};

