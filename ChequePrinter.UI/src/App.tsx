import React from 'react';
import { ChequeService } from './services/cheques/cheque.service';
import { DependencyService } from './services/dependencies/dependency.service';

export class App extends React.Component<AppProps, AppState> {
    private chequeService = DependencyService.getInstance().getDependency<ChequeService>(ChequeService.serviceName);
    private chequeForm: React.RefObject<HTMLFormElement>;

    constructor(props: AppProps) {
        super(props);

        this.state = {
            chequeValue: '',
            result: '',
            formSubmitted: false
        };

        this.chequeForm = React.createRef<HTMLFormElement>();
    }

    setChequeValue(event: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ chequeValue: event.target.value });
    }

    async printCheque(event: React.ChangeEvent<HTMLFormElement>) {
        event.preventDefault();

        this.setState({ formSubmitted: true });

        if (this.chequeForm.current && this.chequeForm.current.checkValidity()) {
            const result = await this.chequeService.post(parseFloat(this.state.chequeValue));
            this.setState({ result: result });
        }
    }

    render() {
        return (
            <main>
                <h1>Cheque Printing Service</h1>
                <form id="cheque-form" ref={this.chequeForm} className={`needs-validation ${this.state.formSubmitted ? 'was-validated' : ''}`} noValidate onSubmit={this.printCheque.bind(this)}>
                    <div className="container-fluid">
                        <div className="row form-group">
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="cheque-value" className="col-form-label">Value:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <input id="cheque-value" type="number" className="form-control" required value={this.state.chequeValue} onChange={this.setChequeValue.bind(this)} />
                                <div className="invalid-feedback">
                                    Please provide a numeric value.
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-lg-10 col-sm-9 col-12 offset-lg-2 offset-sm-3">
                                <button type="submit" className="btn btn-primary">Submit</button>
                            </div>
                        </div>
                        <div className={`row form-group mt-sm-3 ${this.state.result === '' ? 'd-none' : ''}`}>
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="printed-cheque" className="col-form-label">Result:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <textarea id="printed-cheque" readOnly className="form-control" value={this.state.result} />
                            </div>
                        </div>
                    </div>
                </form>
            </main>
        );
    }
}

interface AppProps { }

interface AppState {
    chequeValue: string;
    result: string;
    formSubmitted: boolean;
}