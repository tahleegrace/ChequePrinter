import React from 'react';

export class App extends React.Component<AppProps, AppState> {
    render() {
        return (
            <main>
                <h1>Cheque Printing Service</h1>
                <form id="cheque-form">
                    <div className="container-fluid">
                        <div className="row form-group">
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="cheque-value" className="col-form-label">Value:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <input id="cheque-value" type="number" className="form-control" required />
                                <div className="invalid-feedback">
                                    Please provide a value.
                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-lg-10 col-sm-9 col-12 offset-lg-2 offset-sm-3">
                                <button type="submit" className="btn btn-primary">Submit</button>
                            </div>
                        </div>
                        <div className="row form-group mt-sm-3">
                            <div className="col-lg-2 col-sm-3 col-12">
                                <label htmlFor="printed-cheque" className="col-form-label">Result:</label>
                            </div>
                            <div className="col-lg-10 col-sm-9 col-12">
                                <input id="printed-cheque" type="text" readOnly className="form-control" />
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

}