import { ChequeService } from "./cheques/cheque.service";
import { DependencyService } from "./dependencies/dependency.service";

function configureDependencies() {
    DependencyService.getInstance().setDependency(ChequeService.serviceName, new ChequeService());
}

export default configureDependencies;