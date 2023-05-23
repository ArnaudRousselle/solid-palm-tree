/* tslint:disable */
/* eslint-disable */
/**
 * MyPersonalAccounting
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import {
    BillingsProjectionArgs,
    instanceOfBillingsProjectionArgs,
    BillingsProjectionArgsFromJSON,
    BillingsProjectionArgsFromJSONTyped,
    BillingsProjectionArgsToJSON,
} from './BillingsProjectionArgs';
import {
    PortfoliosProjectionArgs,
    instanceOfPortfoliosProjectionArgs,
    PortfoliosProjectionArgsFromJSON,
    PortfoliosProjectionArgsFromJSONTyped,
    PortfoliosProjectionArgsToJSON,
} from './PortfoliosProjectionArgs';
import {
    RepetitiveBillingsProjectionArgs,
    instanceOfRepetitiveBillingsProjectionArgs,
    RepetitiveBillingsProjectionArgsFromJSON,
    RepetitiveBillingsProjectionArgsFromJSONTyped,
    RepetitiveBillingsProjectionArgsToJSON,
} from './RepetitiveBillingsProjectionArgs';

/**
 * @type ProjectionArgs
 * 
 * @export
 */
export type ProjectionArgs = { argsType: 'BillingsProjectionArgs' } & BillingsProjectionArgs | { argsType: 'PortfoliosProjectionArgs' } & PortfoliosProjectionArgs | { argsType: 'RepetitiveBillingsProjectionArgs' } & RepetitiveBillingsProjectionArgs;

export function ProjectionArgsFromJSON(json: any): ProjectionArgs {
    return ProjectionArgsFromJSONTyped(json, false);
}

export function ProjectionArgsFromJSONTyped(json: any, ignoreDiscriminator: boolean): ProjectionArgs {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    switch (json['argsType']) {
        case 'BillingsProjectionArgs':
            return {...BillingsProjectionArgsFromJSONTyped(json, true), argsType: 'BillingsProjectionArgs'};
        case 'PortfoliosProjectionArgs':
            return {...PortfoliosProjectionArgsFromJSONTyped(json, true), argsType: 'PortfoliosProjectionArgs'};
        case 'RepetitiveBillingsProjectionArgs':
            return {...RepetitiveBillingsProjectionArgsFromJSONTyped(json, true), argsType: 'RepetitiveBillingsProjectionArgs'};
        default:
            throw new Error(`No variant of ProjectionArgs exists with 'argsType=${json['argsType']}'`);
    }
}

export function ProjectionArgsToJSON(value?: ProjectionArgs | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    switch (value['argsType']) {
        case 'BillingsProjectionArgs':
            return BillingsProjectionArgsToJSON(value);
        case 'PortfoliosProjectionArgs':
            return PortfoliosProjectionArgsToJSON(value);
        case 'RepetitiveBillingsProjectionArgs':
            return RepetitiveBillingsProjectionArgsToJSON(value);
        default:
            throw new Error(`No variant of ProjectionArgs exists with 'argsType=${value['argsType']}'`);
    }

}

