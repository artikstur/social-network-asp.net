import axios from "axios";
import {API_BASE_URL} from "../constants.ts";
import {User} from "../interfaces/models/User.ts";
import {ChangeUserRolesRequest} from "../interfaces/requests/ChangeUserRolesRequest.ts";
import {ApiResponse} from "../interfaces/responses/ApiResponse.ts";

export const checkAdminAccess = async (): Promise<boolean> => {
    try {
        const response = await axios.get<ApiResponse<string>>(`${API_BASE_URL}/api/admin`, {
            withCredentials: true,
        });
        console.log(response.status)
        if (response.status === 200) {
            return true;
        } else {
            return false;
        }
    } catch (error) {
        console.error('Error checking admin access:', error);
        return false;
    }
};

export const fetchUsers = async (): Promise<User[]> => {
    try {
        const response = await axios.get<ApiResponse<User[]>>(`${API_BASE_URL}/api/admin/users`, {
            withCredentials: true,
        });
        if (response.status === 200) {
            const {result, errors} = response.data;

            if (errors.length > 0) {
                throw new Error(errors.join(", "));
            }
            return result;
        } else {
            throw Error;
        }
    } catch (e) {
        console.error("Error fetching users:", e);
        throw e;
    }
};

export const fetchUserRoles = async (userId: string): Promise<string[]> => {
    try {
        const response = await axios.get<ApiResponse<string[]>>(`${API_BASE_URL}/api/admin/${userId}/roles`, {
            withCredentials: true,
        });

        if (response.status === 200) {
            const {result, errors} = response.data;

            if (errors.length > 0) {
                throw new Error(errors.join(", "));
            }
            return result;
        } else {
            throw Error;
        }
    } catch (e) {
        console.error("Error fetching users:", e);
        throw e;
    }
};

export const changeUserRoles = async (data: ChangeUserRolesRequest): Promise<void> => {
    try {
        const response = await axios.put(`${API_BASE_URL}/api/admin/update-roles`, data, {
            withCredentials: true,
        });

        if (response.status !== 200) {
            throw Error;
        }
    } catch (error) {
        console.error("Error updating user roles:", error);
        throw error;
    }
};
