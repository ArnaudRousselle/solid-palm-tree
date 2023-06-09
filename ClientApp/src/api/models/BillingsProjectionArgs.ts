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

import { exists, mapValues } from '../runtime';
/**
 * 
 * @export
 * @interface BillingsProjectionArgs
 */
export interface BillingsProjectionArgs {
    /**
     * 
     * @type {string}
     * @memberof BillingsProjectionArgs
     */
    readonly argsType: string;
    /**
     * 
     * @type {number}
     * @memberof BillingsProjectionArgs
     */
    version: number;
    /**
     * 
     * @type {string}
     * @memberof BillingsProjectionArgs
     */
    portfolioId: string;
}

/**
 * Check if a given object implements the BillingsProjectionArgs interface.
 */
export function instanceOfBillingsProjectionArgs(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "argsType" in value;
    isInstance = isInstance && "version" in value;
    isInstance = isInstance && "portfolioId" in value;

    return isInstance;
}

export function BillingsProjectionArgsFromJSON(json: any): BillingsProjectionArgs {
    return BillingsProjectionArgsFromJSONTyped(json, false);
}

export function BillingsProjectionArgsFromJSONTyped(json: any, ignoreDiscriminator: boolean): BillingsProjectionArgs {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'argsType': json['argsType'],
        'version': json['version'],
        'portfolioId': json['portfolioId'],
    };
}

export function BillingsProjectionArgsToJSON(value?: BillingsProjectionArgs | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'version': value.version,
        'portfolioId': value.portfolioId,
    };
}

