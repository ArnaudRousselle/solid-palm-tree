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
 * @interface CreatePortfolioRequest
 */
export interface CreatePortfolioRequest {
    /**
     * 
     * @type {string}
     * @memberof CreatePortfolioRequest
     */
    name: string;
}

/**
 * Check if a given object implements the CreatePortfolioRequest interface.
 */
export function instanceOfCreatePortfolioRequest(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "name" in value;

    return isInstance;
}

export function CreatePortfolioRequestFromJSON(json: any): CreatePortfolioRequest {
    return CreatePortfolioRequestFromJSONTyped(json, false);
}

export function CreatePortfolioRequestFromJSONTyped(json: any, ignoreDiscriminator: boolean): CreatePortfolioRequest {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'name': json['name'],
    };
}

export function CreatePortfolioRequestToJSON(value?: CreatePortfolioRequest | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'name': value.name,
    };
}

