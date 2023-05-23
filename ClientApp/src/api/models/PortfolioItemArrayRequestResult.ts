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
import type { PortfolioItem } from './PortfolioItem';
import {
    PortfolioItemFromJSON,
    PortfolioItemFromJSONTyped,
    PortfolioItemToJSON,
} from './PortfolioItem';

/**
 * 
 * @export
 * @interface PortfolioItemArrayRequestResult
 */
export interface PortfolioItemArrayRequestResult {
    /**
     * 
     * @type {Array<PortfolioItem>}
     * @memberof PortfolioItemArrayRequestResult
     */
    result: Array<PortfolioItem>;
    /**
     * 
     * @type {number}
     * @memberof PortfolioItemArrayRequestResult
     */
    version: number;
}

/**
 * Check if a given object implements the PortfolioItemArrayRequestResult interface.
 */
export function instanceOfPortfolioItemArrayRequestResult(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "result" in value;
    isInstance = isInstance && "version" in value;

    return isInstance;
}

export function PortfolioItemArrayRequestResultFromJSON(json: any): PortfolioItemArrayRequestResult {
    return PortfolioItemArrayRequestResultFromJSONTyped(json, false);
}

export function PortfolioItemArrayRequestResultFromJSONTyped(json: any, ignoreDiscriminator: boolean): PortfolioItemArrayRequestResult {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'result': ((json['result'] as Array<any>).map(PortfolioItemFromJSON)),
        'version': json['version'],
    };
}

export function PortfolioItemArrayRequestResultToJSON(value?: PortfolioItemArrayRequestResult | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'result': ((value.result as Array<any>).map(PortfolioItemToJSON)),
        'version': value.version,
    };
}
