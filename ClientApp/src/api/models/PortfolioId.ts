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
 * @interface PortfolioId
 */
export interface PortfolioId {
    /**
     * 
     * @type {string}
     * @memberof PortfolioId
     */
    readonly guid?: string;
}

/**
 * Check if a given object implements the PortfolioId interface.
 */
export function instanceOfPortfolioId(value: object): boolean {
    let isInstance = true;

    return isInstance;
}

export function PortfolioIdFromJSON(json: any): PortfolioId {
    return PortfolioIdFromJSONTyped(json, false);
}

export function PortfolioIdFromJSONTyped(json: any, ignoreDiscriminator: boolean): PortfolioId {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'guid': !exists(json, 'guid') ? undefined : json['guid'],
    };
}

export function PortfolioIdToJSON(value?: PortfolioId | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
    };
}

