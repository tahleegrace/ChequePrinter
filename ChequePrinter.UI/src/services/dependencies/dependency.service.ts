import * as _ from "lodash";

export class DependencyService {
    private static _instance = new DependencyService();
    private _dependencies = new Map<string, any>();

    private DependencyService() { }

    public getDependency<DependencyType>(key: string): DependencyType {
        return this._dependencies.get(key);
    }

    public setDependency<DependencyType>(key: string, dependency: DependencyType) {
        if (_.isNil(dependency)) {
            throw new Error('Dependency cannot be null or undefined.');
        }

        this._dependencies.set(key, dependency);
    }

    static getInstance() {
        return DependencyService._instance;
    }
}