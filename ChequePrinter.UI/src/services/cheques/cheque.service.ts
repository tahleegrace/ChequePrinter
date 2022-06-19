import config from "../../config";
import { HttpHeaders } from "../../interfaces/http-headers";

export class ChequeService {
    static serviceName = 'cheque-service';

    getHeaders(): HttpHeaders {
        const headers: HttpHeaders = {
            'Content-Type': 'application/json',
        };

        return headers;
    }

    public async post(body: number): Promise<string> {
        const response = await fetch(`${config.api.url}/cheques/print`, {
            method: 'POST',
            headers: this.getHeaders() as any,
            body: body.toString()
        });

        return await response.text();
    }
}